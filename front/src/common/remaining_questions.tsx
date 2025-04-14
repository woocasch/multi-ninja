import React, { useEffect, useMemo, useState } from 'react';
import './remaining_questions.scss';

export interface Properties {
  answeredQuestions: number;
  totalQuestions: number;
}

export default function RemainingQuestionsComponent(props: Properties) {
  const [containerClass, setContainerClass] = useState<string>('display');

  const questionsRemaining = useMemo(() => {
    return props.totalQuestions - props.answeredQuestions;
  }, [props.totalQuestions, props.answeredQuestions]);

  const animationTrigger = useEffect(() => {
    if (props.answeredQuestions == 0) {
      return;
    }

    setContainerClass('display question-switched');
    setTimeout(() => setContainerClass('display'), 1000);
  }, [questionsRemaining]);

  return (
    <div className="remaining-questions">
      <div className={containerClass}>{questionsRemaining}</div>
    </div>
  );
}
