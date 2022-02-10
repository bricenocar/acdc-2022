import { Answer } from "../Models/Answer";
import { Participation } from "../Models/Participation";

export interface ParticipationDTO{
    participation: Participation;
    answers: Answer[];
}