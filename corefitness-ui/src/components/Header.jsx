import Navbar from "./Navbar";
import "./Header.css"
import Logo from "../images/logotype.svg"

export default function Header() {
    return (
        // -------- HOME ---- Header ----------- //
        <header>
            <div className="header-logo"> 
                <img className='logotype' src={Logo} alt='logotype'/>
            </div>

            <Navbar />

            <div className="header-actions"> 
                <div>Auth</div>
            </div>

        </header>
    )
}