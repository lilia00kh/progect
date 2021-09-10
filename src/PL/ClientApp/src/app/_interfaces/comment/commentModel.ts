import { AnswerToCommentModel } from './answerToCommentModel';

export interface CommentModel {
    id: string;
    userName: string;
    text: string;
    date: Date;
    answerToCommentModels: AnswerToCommentModel[];
 }