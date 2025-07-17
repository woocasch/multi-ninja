import {
  CalculateGameScoreParameters,
  CheckGameCompletedParameters,
  CommonGameLogicService,
  SelectQuestionParameters,
  StartGameParameters,
  ValidateAnswerParameters,
} from '../common/common_game_logic';
import * as QuestionGenerator from '../common/questions_generator';

export const GameLogic: CommonGameLogicService = new CommonGameLogicService(
  '*',
  (q) => q,
  (l, r) => l * r,
  QuestionGenerator.generatorFactory(),
);

export type {
  ValidateAnswerParameters,
  CheckGameCompletedParameters,
  CalculateGameScoreParameters,
  StartGameParameters,
  SelectQuestionParameters,
};
