import Navbar from "./Navbar";
import "./Header.css"
import Logo from "../images/logotype.svg"
import { Link } from "react-router-dom"
import Button from "../components/Button";


export default function Header() {
    return (
        // -------- HOME ---- Header ----------- //
        <header>
            <div className="header-logo"> 
                <img className='logotype' src={Logo} alt='logotype'/>
            </div>

            <Navbar />

            <div className="header-actions"> 
                <Link to="/signup">Become Member</Link>

                <Button>Sign In</Button>
            </div>

        </header>
    )
}