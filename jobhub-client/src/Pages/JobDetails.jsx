import { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import api from "../Services/api";

function JobDetails() {

    const { id } = useParams();

    const [job, setJob] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState("");

    useEffect(() => {

        const fetchJob = async () => {

            try {

                const response = await api.get(`/Job/${id}`);

                setJob(response.data);

            }
            catch (error) {

                setError("Failed to load job.");

            }
            finally {

                setLoading(false);

            }
        };

        fetchJob();

    }, [id]);

    if (loading) {
        return <h3 className="text-center mt-5">Loading...</h3>;
    }

    if (error) {
        return <h3 className="text-center mt-5">{error}</h3>;
    }

    if (!job) {
        return <h3 className="text-center mt-5">Job not found</h3>;
    }

    return (
        <div className="container mt-5">

            <div className="card p-4">

                <h2>{job.title}</h2>

                <hr />

                <p>
                    <strong>Description:</strong>
                    <br />
                    {job.description}
                </p>

                <p>
                    <strong>Location:</strong> {job.location}
                </p>

                <p>
                    <strong>Salary:</strong> ₹{job.salary}
                </p>

                <p>
                    <strong>Company:</strong> {job.companyName}
                </p>

            </div>

        </div>
    );
}

export default JobDetails;