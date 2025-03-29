import './game_board.css';

export default function RootLayout({
    children,
}: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="en">
            <body>
                <div id="board">
                    {children}
                </div>
            </body>
        </html>
    );
}
