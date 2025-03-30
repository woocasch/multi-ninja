import { DifficultyLevel } from "./game_manager";

interface EnumerationLocalizationEntry<TEnum> {
    value: TEnum;
    text: string;
}

interface Language<TEnum> {
    languageCode: string;
    localizations: EnumerationLocalizationEntry<TEnum>[];
}

interface LocalizationList<TEnum> {
    languages: Language<TEnum>[];
}

const difficultyLevelTexts: LocalizationList<DifficultyLevel> = {
    languages: [
        {
            languageCode: 'pl-PL',
            localizations: [
                { value: DifficultyLevel.Easy, text: 'Łatwy' },
                { value: DifficultyLevel.Medium, text: 'Średni' },
                { value: DifficultyLevel.Hard, text: 'Trudny' },
            ]
        }
    ]
};

export enum StaticTexts {
    BtnStartGame_Text = 1,
    BtnAcceptRespose_Text = 2,
}

interface StaticTextTranslation {
    textId: StaticTexts;
    translation: string;
}

interface StaticTextLanguage {
    languageCode: string;
    translations: StaticTextTranslation[];
}

interface StaticTextList {
    languages: StaticTextLanguage[];
}

const staticTexts: StaticTextList = {
    languages: [
        {
            languageCode: 'pl-PL',
            translations: [
                { textId: StaticTexts.BtnStartGame_Text, translation: 'Start gry' },
                { textId: StaticTexts.BtnAcceptRespose_Text, translation: 'Zatwierdź' }
            ]
        }
    ]
}

class LocalizationsService {
    private defaultLanguage = 'pl-PL';

    public TranslateStaticText(textId: StaticTexts, lang = 'pl-PL'): string {
        const languageTranslations = this.FindStaticTextLanguage(lang);
        const translation = this.FindStaticTextTranslation(languageTranslations, textId);
        if (!!translation) {
            return translation;
        }

        const defaultTranslations = this.FindStaticTextLanguage(this.defaultLanguage);
        return this.FindStaticTextTranslation(defaultTranslations, textId) || '---';
    }

    public GetDifficultyLevelText(level: DifficultyLevel, lang = 'pl-PL'): string {
        return this.FindLocalization(
            difficultyLevelTexts,
            level,
            lang);
    }

    private FindLocalization<TEnum>(
        list: LocalizationList<TEnum>,
        value: TEnum,
        lang: string) {
        const languageTranslations = this.FindLanguage<TEnum>(list, lang);
        const translation = this.FindTranslation(languageTranslations, value);
        if (!!translation) {
            return translation;
        }

        const defaultTranslations = this.FindLanguage<TEnum>(list, this.defaultLanguage);
        return this.FindTranslation(defaultTranslations, value) || '---';
    }

    private FindLanguage<TEnum>(list: LocalizationList<TEnum>, lang: string) {
        let languageIndex = list.languages.findIndex(v => v.languageCode == lang);
        if (languageIndex == -1) {
            languageIndex = list.languages.findIndex(v => v.languageCode == this.defaultLanguage);
        }

        return list.languages[languageIndex];
    }

    private FindTranslation<TEnum>(translations: Language<TEnum>, value: TEnum) {
        const translationIndex = translations.localizations.findIndex(v => v.value == value);
        if (translationIndex == -1) {
            return null;
        }

        return translations.localizations[translationIndex].text;
    }

    private FindStaticTextLanguage(lang: string) {
        let languageIndex = staticTexts.languages.findIndex(v => v.languageCode == lang);
        if (languageIndex == -1) {
            languageIndex = staticTexts.languages.findIndex(v => v.languageCode == this.defaultLanguage);
        }

        return staticTexts.languages[languageIndex];
    }

    private FindStaticTextTranslation(translations: StaticTextLanguage, value: StaticTexts) {
        const translationIndex = translations.translations.findIndex(v => v.textId == value);
        if (translationIndex == -1) {
            return null;
        }

        return translations.translations[translationIndex].translation;
    }
}

export const Localizations: LocalizationsService = new LocalizationsService();