import { Link } from "react-router-dom";

function Navbar({ brandName }) {

  return (
    <nav className="navbar navbar-expand-lg bg-dark navbar-dark">

      <div className="container">

        <Link
          className="navbar-brand"
          to="/"
        >
          {brandName}
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

        </div>

      </div>

    </nav>
  );
}

export default Navbar;