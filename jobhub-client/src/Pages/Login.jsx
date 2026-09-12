import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../Services/api";
import loginSchema from "../validations/loginSchema";

function Login() {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        email: "",
        password: ""
    });

    const [errors, setErrors] = useState({});
    const [serverError, setServerError] = useState("");

    const handleChange = (event) => {
        const { name, value } = event.target;

        setFormData({
            ...formData,
            [name]: value
        });

        setErrors({
            ...errors,
            [name]: ""
        });
    };

    const handleLogin = async (event) => {
        event.preventDefault();

        setServerError("");

        try {
            await loginSchema.validate(formData, {
                abortEarly: false
            });

            setErrors({});

            const response = await api.post(
                "/Auth/login",
                {
                    email: formData.email,
                    password: formData.password
                }
            );

            const token = response.data.token;

            localStorage.setItem("token", token);

            navigate("/jobs");
        } catch (error) {
            if (error.name === "ValidationError") {
                const validationErrors = {};

                error.inner.forEach((validationError) => {
                    validationErrors[validationError.path] =
                        validationError.message;
                });

                setErrors(validationErrors);
            } else {
                setServerError(
                    error.response?.data?.message ||
                    "Invalid email or password."
                );
            }
        }
    };

    return (
        <div className="container mt-5">

            <div className="row justify-content-center">

                <div className="col-md-6">

                    <div className="card p-4">

                        <h2 className="mb-4">
                            Login
                        </h2>

                        <form onSubmit={handleLogin}>

                            <div className="mb-3">

                                <label className="form-label">
                                    Email
                                </label>

                                <input
                                    type="email"
                                    name="email"
                                    className="form-control"
                                    value={formData.email}
                                    onChange={handleChange}
                                />

                                {errors.email && (
                                    <div className="text-danger mt-1">
                                        {errors.email}
                                    </div>
                                )}

                            </div>

                            <div className="mb-3">

                                <label className="form-label">
                                    Password
                                </label>

                                <input
                                    type="password"
                                    name="password"
                                    className="form-control"
                                    value={formData.password}
                                    onChange={handleChange}
                                />

                                {errors.password && (
                                    <div className="text-danger mt-1">
                                        {errors.password}
                                    </div>
                                )}

                            </div>

                            <button
                                type="submit"
                                className="btn btn-primary w-100"
                            >
                                Login
                            </button>

                        </form>

                        {serverError && (
                            <div className="alert alert-danger mt-3">
                                {serverError}
                            </div>
                        )}

                    </div>

                </div>

            </div>

        </div>
    );
}

export default Login;