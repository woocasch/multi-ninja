import Image from "next/image";
import styles from "./page.module.css";
import Link from "next/link";

export default function Home() {
  return (
    <div className="HomePage">
      <h2>
        Welcome to MultiNinja
      </h2>
      <p>
        To start new game click <Link href={`/game_board`}>here</Link>
      </p>
    </div>
  );
}
