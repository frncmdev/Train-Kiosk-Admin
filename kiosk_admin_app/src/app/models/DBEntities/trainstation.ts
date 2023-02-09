import { ICampus } from "./campus";
export interface ITrainStation
{
  trainstationId: number;
  trainstationName: string;
  travelTime: number;
  campuses: ICampus[];

}
