import { Outlet } from "react-router";

export default function AppLayoutComponent() {
    return(  <div className="root_container">
    <div className="menu">
      <a href="/">Start</a>
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