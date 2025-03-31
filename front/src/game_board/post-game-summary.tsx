import React from "react";

import { GameManager } from "./game_manager";

export default function PostGameSummaryView() {
    return (
        <div>
            <h2>Round completed</h2>
            <p>Your results:</p>
            <ul>
                {GameManager.GetAnsweredQuestions().map((q, i) => (
                    <li key={i}>
                        {q.LeftFactor} * {q.RightFactor} = {q.ExpectedAnswer}
                        <p>Your answers:</p>
                        <ul>
                            {q.ProvidedAnswers.map((nq, ni) => (
                                <li key={`${i}_${ni}`}>
                                    {nq}
                                </li>
                            ))}
                        </ul>
                    </li>
                ))}
            </ul>
        </div>
    )
}