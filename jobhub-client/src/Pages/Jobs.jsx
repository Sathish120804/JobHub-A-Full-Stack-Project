import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import api from "../Services/api";

function Jobs() {
    const [jobs, setJobs] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {
        const fetchJobs = async () => {
            try {
                const response = await api.get("/Job");

                setJobs(response.data);
            } catch (error) {
                setError("Failed to load jobs.");
            } finally {
                setLoading(false);
            }
        };

        fetchJobs();
    }, []);

    if (loading) {
        return (
            <h3 className="text-center mt-5">
                Loading jobs...
            </h3>
        );
    }

    if (error) {
        return (
            <h3 className="text-center mt-5">
                {error}
            </h3>
        );
    }

    return (
        <div className="container mt-4">

            <h2 className="mb-4">
                Available Jobs
            </h2>

            <div className="row">

                {jobs.map((job) => (
                    <div
                        className="col-md-4 mb-4"
                        key={job.id}
                    >

                        <div className="card h-100 p-3">

                            <h5>{job.title}</h5>

                            <p>
                                {job.description}
                            </p>

                            <p>
                                <strong>Location:</strong>{" "}
                                {job.location}
                            </p>

                            <p>
                                <strong>Salary:</strong>{" "}
                                ₹{job.salary}
                            </p>

                            <p>
                                <strong>Company:</strong>{" "}
                                {job.companyName}
                            </p>

                            <Link
                                to={`/jobs/${job.id}`}
                                className="btn btn-primary"
                            >
                                View Details
                            </Link>

                        </div>

                    </div>
                ))}

            </div>

        </div>
    );
}

export default Jobs;