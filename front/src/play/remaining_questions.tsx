import React, { useMemo } from "react";
import './remaining_questions.css';

export interface Properties {
    answeredQuestions: number;
    totalQuestions: number;
}

export default function RemainingQuestionsComponent(props: Properties) {
    const questionsRemaining = useMemo(() => {
        return props.totalQuestions - props.answeredQuestions;
    }, [props.totalQuestions, props.answeredQuestions]);

    return (
        <div className="questions-display">
            {questionsRemaining}
        </div>
    );
}