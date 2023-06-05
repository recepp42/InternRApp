import { TrainingType } from "../enums/trainingType"
import { InternshipTranslationUpdatePostDto } from "./internshipTranslationUpdatePostDto"
import { LocationItem } from "./locationItem"

export interface InternshipUpdateDto{
    internShipId:number
    schoolYear:string 
    unitId:string
    maxCountOfStudents:number 
    trainingType:TrainingType 
    currentCountOfStudents:number,
    locations:LocationItem[],
    versions:InternshipTranslationUpdatePostDto[]
}