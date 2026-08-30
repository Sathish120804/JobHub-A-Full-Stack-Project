import Navbar from "./Components/Navbar";
import Home from "./Pages/Home";
import Jobs from "./Pages/Jobs";
import Login from "./Pages/Login";
import Register from "./Pages/Register";
import Notfound from "./Pages/Notfound";
import { BrowserRouter,Routes,Route } from "react-router-dom";


function App() {
  return (
    <>
      <BrowserRouter>
      <Navbar brandName="JobHub" />
        <Routes>
          <Route path="/" element={<Home />}></Route>
          <Route path="/jobs" element={<Jobs />}></Route>
          <Route path="/login" element={<Login />} />

          <Route path="/register" element={<Register />} />

          <Route path="*" element={<Notfound />} />
        </Routes>
      </BrowserRouter>
      


    </>
  );
}

export default App;