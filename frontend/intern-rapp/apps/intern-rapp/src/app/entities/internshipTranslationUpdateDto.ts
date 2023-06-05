import { LanguageItem } from "./languageItem"
import { LanguageWithMinimalData } from "./languageWithMinimalData"

export interface InternshipTranslationUpdateDto{
    titleContent: string
    description: string
    knowledgeToDevelop: string
    neededKnowledge: string
    language:LanguageItem
    location: string
    comment: string
    translationId:number
}