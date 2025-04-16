import {
  CalculateGameScoreParameters,
  CheckGameCompletedParameters,
  CommonGameLogicService,
  SelectQuestionParameters,
  StartGameParameters,
  ValidateAnswerParameters,
} from '../common/common_game_logic';

export const GameLogic: CommonGameLogicService = new CommonGameLogicService(
  '*',
  (q) => q,
  (l, r) => l * r,
);

export type {
  ValidateAnswerParameters,
  CheckGameCompletedParameters,
  CalculateGameScoreParameters,
  StartGameParameters,
  SelectQuestionParameters,
};
