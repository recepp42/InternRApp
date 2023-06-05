import { LanguageItem } from './languageItem';

export interface PrefaceTranslationUpdateUnit {
    content: string;
    language: LanguageItem;
    translationId:number
}
