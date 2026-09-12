import * as Yup from "yup";

const registerSchema = Yup.object({
    name: Yup.string()
        .min(2, "Name must contain at least 2 characters")
        .max(50, "Name cannot exceed 50 characters")
        .required("Name is required"),

    email: Yup.string()
        .email("Please enter a valid email address")
        .required("Email is required"),

    password: Yup.string()
        .min(6, "Password must contain at least 6 characters")
        .required("Password is required"),

    confirmPassword: Yup.string()
        .oneOf(
            [Yup.ref("password")],
            "Passwords must match"
        )
        .required("Please confirm your password")
});

export default registerSchema;