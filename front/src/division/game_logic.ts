import {
  CalculateGameScoreParameters,
  CheckGameCompletedParameters,
  CommonGameLogicService,
  SelectQuestionParameters,
  StartGameParameters,
  ValidateAnswerParameters,
} from '../common/common_game_logic';
import * as Model from '../common/types';

export const GameLogic: CommonGameLogicService = new CommonGameLogicService(
  '÷',
  (q) => {
    return <Model.Question>{
      leftHand: q.leftHand * q.rightHand,
      rightHand: q.rightHand,
    };
  },
  (l, r) => l / r,
);

export type {
  ValidateAnswerParameters,
  CheckGameCompletedParameters,
  CalculateGameScoreParameters,
  StartGameParameters,
  SelectQuestionParameters,
};
