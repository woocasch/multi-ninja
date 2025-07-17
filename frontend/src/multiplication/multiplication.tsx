import React, { useEffect, useMemo, useState } from 'react';
import GameScreen from '../common/game_screen';
import * as Logic from './game_logic';

export default function MultiplicationComponent() {
  return <GameScreen logic={Logic.GameLogic} />;
}
