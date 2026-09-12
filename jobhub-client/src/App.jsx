import Navbar from "./Components/Navbar";
import Home from "./Pages/Home";
import Jobs from "./Pages/Jobs";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import NotFound from "./Pages/Notfound";
import JobDetails from "./Pages/JobDetails";
import ProtectedRoute from "./Components/ProtectedRoute";

import { BrowserRouter, Routes, Route } from "react-router-dom";

function App() {
    return (
        <BrowserRouter>
            <Navbar brandName="JobHub" />

            <Routes>
                <Route
                    path="/"
                    element={<Home />}
                />

                <Route
                    path="/jobs"
                    element={
                        <ProtectedRoute>
                            <Jobs />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/jobs/:id"
                    element={
                        <ProtectedRoute>
                            <JobDetails />
                        </ProtectedRoute>
                    }
                />

                <Route
                    path="/login"
                    element={<Login />}
                />

                <Route
                    path="/register"
                    element={<Register />}
                />

                <Route
                    path="*"
                    element={<NotFound />}
                />
            </Routes>
        </BrowserRouter>
    );
}

export default App;