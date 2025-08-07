import { NavLink, Outlet } from "react-router";
import UserDisplayComponent from "./auth/user-display";
import './AppLayout.scss';

export default function AppLayoutComponent() {
    return(  <div className="root_container">
    <div className="menu">
      <NavLink to="/">Start</NavLink>
      <UserDisplayComponent />
    </div>
        <Outlet />
    <div className="footer">
      Znalazłeś błąd? Opisz go{' '}
      <a
        href="https://github.com/woocasch/multi-ninja/issues/new"
        target="_blank"
      >
        tutaj
      </a>
      .
    </div>
  </div>);
}