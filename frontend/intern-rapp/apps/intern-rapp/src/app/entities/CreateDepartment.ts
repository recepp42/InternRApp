import { PrefaceTranslationCreateUnit } from "./prefaceTranslationCreateUnit";

export interface CreateDepartment {
  name: string | null;
  superVisorEmails: string[];
  prefaces: PrefaceTranslationCreateUnit[] | null;
}
