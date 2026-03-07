import "./Button.css"
import ArrowRight from "../images/arrow-right.svg"

export default function Button({ children, onClick }) {
    return (
        <button className="btn" onClick={onClick}>
            {children}
            <img src={ArrowRight} alt="arrow-right" />
        </button>
    )
}