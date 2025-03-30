import './game_board.css';

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <div id="board">
            {children}
        </div>
    );
}
