import { useState } from "react";
import { useNavigate } from "react-router-dom";
import api from "../Services/api";
import registerSchema from "../validations/registerSchema";

function Register() {
    const navigate = useNavigate();

    const [formData, setFormData] = useState({
        name: "",
        email: "",
        password: "",
        confirmPassword: ""
    });

    const [errors, setErrors] = useState({});
    const [message, setMessage] = useState("");
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

    const handleRegister = async (event) => {
        event.preventDefault();

        setMessage("");
        setServerError("");

        try {
            await registerSchema.validate(formData, {
                abortEarly: false
            });

            setErrors({});

            await api.post("/Auth/register", {
                name: formData.name,
                email: formData.email,
                password: formData.password
            });

            setMessage("Registration successful!");

            setTimeout(() => {
                navigate("/login");
            }, 1000);
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
                    "Registration failed."
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
                            Create Account
                        </h2>

                        <form onSubmit={handleRegister}>

                            <div className="mb-3">

                                <label className="form-label">
                                    Name
                                </label>

                                <input
                                    type="text"
                                    name="name"
                                    className="form-control"
                                    value={formData.name}
                                    onChange={handleChange}
                                />

                                {errors.name && (
                                    <div className="text-danger mt-1">
                                        {errors.name}
                                    </div>
                                )}

                            </div>

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

                            <div className="mb-3">

                                <label className="form-label">
                                    Confirm Password
                                </label>

                                <input
                                    type="password"
                                    name="confirmPassword"
                                    className="form-control"
                                    value={formData.confirmPassword}
                                    onChange={handleChange}
                                />

                                {errors.confirmPassword && (
                                    <div className="text-danger mt-1">
                                        {errors.confirmPassword}
                                    </div>
                                )}

                            </div>

                            <button
                                type="submit"
                                className="btn btn-primary w-100"
                            >
                                Register
                            </button>

                        </form>

                        {message && (
                            <div className="alert alert-success mt-3">
                                {message}
                            </div>
                        )}

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

export default Register;