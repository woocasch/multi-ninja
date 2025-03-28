import { NavLink } from 'react-router-dom';
import './HomePage.css';

function HomePage() {
    return (
        <div className="HomePage">
            <h2>
                Welcome to MultiNinja
            </h2>
            <p>
                To start new game click <NavLink to="/game">here</NavLink>
            </p>
        </div>
    );
};

export default HomePage;
