import { InternshipTranslation } from "./internshipTranslation"
import { LocationItem } from "./locationItem"

export interface CreateInternship{
    schoolYear: string
    unitId: number
    maxCountOfStudents: number
    currentCountOfStudents: number
    trainingType: number
    locations: LocationItem[]
    versions: InternshipTranslation[]
}