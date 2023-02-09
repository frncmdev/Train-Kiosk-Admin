import { ITrainStation } from "./trainstation";
export interface ICampus
{
  campusId: number;
  campusName: string;
  isSelected: boolean;
  trainstationId: number;
  trainstation: ITrainStation;
}
