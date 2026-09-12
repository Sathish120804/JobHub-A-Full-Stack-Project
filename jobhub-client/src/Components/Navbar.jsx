import { Link, useNavigate } from "react-router-dom";

function Navbar() {

    const navigate = useNavigate();

    const token = localStorage.getItem("token");

    const handleLogout = () => {

        localStorage.removeItem("token");

        navigate("/login");
    };

    return (
        <nav className="navbar navbar-expand-lg navbar-dark bg-dark">

            <div className="container">

                <Link
                    className="navbar-brand"
                    to="/"
                >
                    JobHub
                </Link>

                <div className="navbar-nav">

                    <Link
                        className="nav-link"
                        to="/"
                    >
                        Home
                    </Link>

                    <Link
                        className="nav-link"
                        to="/jobs"
                    >
                        Jobs
                    </Link>

                    {!token && (
                        <>
                            <Link
                                className="nav-link"
                                to="/login"
                            >
                                Login
                            </Link>

                            <Link
                                className="nav-link"
                                to="/register"
                            >
                                Register
                            </Link>
                        </>
                    )}

                    {token && (
                        <button
                            className="btn btn-danger ms-2"
                            onClick={handleLogout}
                        >
                            Logout
                        </button>
                    )}

                </div>

            </div>

        </nav>
    );
}

export default Navbar;