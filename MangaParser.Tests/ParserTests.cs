using MangaPacker;
using Xunit;

namespace MangaParser.Tests
{
    public class ParserTests
    {
        [Theory]
        [InlineData("Killing Bites Vol. 0001 Ch. 0001 - Galactica Scanlations (gb)", "1")]
        [InlineData("My Girlfriend Is Shobitch v01 - ch. 09 - pg. 008.png", "1")]
        [InlineData("Historys Strongest Disciple Kenichi_v11_c90-98.zip", "11")]
        [InlineData("B_Gata_H_Kei_v01[SlowManga&OverloadScans]", "1")]
        [InlineData("BTOOOM! v01 (2013) (Digital) (Shadowcat-Empire)", "1")]
        [InlineData("Gokukoku no Brynhildr - c001-008 (v01) [TrinityBAKumA]", "1")]
        [InlineData("Dance in the Vampire Bund v16-17 (Digital) (NiceDragon)", "16-17")]
        [InlineData("v001", "1")]
        [InlineData("Akame ga KILL! ZERO v01 (2016) (Digital) (LuCaZ).cbz", "1")]
        [InlineData("No Volume", "0")]
        [InlineData("U12 (Under 12) Vol. 0001 Ch. 0001 - Reiwa Scans (gb)", "1")]
        [InlineData("[Suihei Kiki]_Kasumi_Otoko_no_Ko_[Taruby]_v1.1.zip", "1")]
        [InlineData("Tonikaku Cawaii [Volume 11].cbz", "11")]
        [InlineData("[WS]_Ichiban_Ushiro_no_Daimaou_v02_ch10.zip", "2")]
        [InlineData("[xPearse] Kyochuu Rettou Volume 1 [English] [Manga] [Volume Scans]", "1")]
        [InlineData("Tower Of God S01 014 (CBT) (digital).cbz", "1")]
        [InlineData("Tenjou_Tenge_v17_c100[MT].zip", "17")]
        [InlineData("Shimoneta - Manmaru Hen - c001-006 (v01) [Various].zip", "1")]
        [InlineData("Future Diary v02 (2009) (Digital) (Viz).cbz", "2")]
        [InlineData("Mujaki no Rakuen Vol12 ch76", "12")]
        [InlineData("Ichinensei_ni_Nacchattara_v02_ch11_[Taruby]_v1.3.zip", "2")]
        [InlineData("Dorohedoro v01 (2010) (Digital) (LostNerevarine-Empire).cbz", "1")]
        [InlineData("Dorohedoro v11 (2013) (Digital) (LostNerevarine-Empire).cbz", "11")]
        [InlineData("Dorohedoro v12 (2013) (Digital) (LostNerevarine-Empire).cbz", "12")]
        [InlineData("Yumekui_Merry_v01_c01[Bakayarou-Kuu].rar", "1")]
        [InlineData("Yumekui-Merry_DKThias_Chapter11v2.zip", "0")]
        [InlineData("Gahi-chan! Vol. 0000 Ch. 0001 - Peruvian Bad Quality Translations (gb)", "0")]
        public void ParseVolumeTest(string filename, string expected)
        {
            var result = Parser.ParseVolume(filename);
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("Killing Bites Vol. 0001 Ch. 0001 - Galactica Scanlations (gb)", "Killing Bites")]
        [InlineData("My Girlfriend Is Shobitch v01 - ch. 09 - pg. 008.png", "My Girlfriend Is Shobitch")]
        [InlineData("Historys Strongest Disciple Kenichi_v11_c90-98.zip", "Historys Strongest Disciple Kenichi")]
        [InlineData("B_Gata_H_Kei_v01[SlowManga&OverloadScans]", "B Gata H Kei")]
        [InlineData("BTOOOM! v01 (2013) (Digital) (Shadowcat-Empire)", "BTOOOM!")]
        [InlineData("Gokukoku no Brynhildr - c001-008 (v01) [TrinityBAKumA]", "Gokukoku no Brynhildr")]
        [InlineData("Dance in the Vampire Bund v16-17 (Digital) (NiceDragon)", "Dance in the Vampire Bund")]
        [InlineData("v001", "")]
        [InlineData("U12 (Under 12) Vol. 0001 Ch. 0001 - Reiwa Scans (gb)", "U12 (Under 12)")]
        [InlineData("Akame ga KILL! ZERO (2016-2019) (Digital) (LuCaZ)", "Akame ga KILL! ZERO")]
        [InlineData("APOSIMZ 017 (2018) (Digital) (danke-Empire).cbz", "APOSIMZ")]
        [InlineData("Akiiro Bousou Biyori - 01.jpg", "Akiiro Bousou Biyori")]
        [InlineData("Beelzebub_172_RHS.zip", "Beelzebub")]
        [InlineData("Dr. STONE 136 (2020) (Digital) (LuCaZ).cbz", "Dr. STONE")]
        [InlineData("Cynthia the Mission 29.rar", "Cynthia the Mission")]
        [InlineData("Darling in the FranXX - Volume 01.cbz", "Darling in the FranXX")]
        [InlineData("Darwin's Game - Volume 14 (F).cbz", "Darwin's Game")]
        [InlineData("[BAA]_Darker_than_Black_c7.zip", "Darker than Black")]
        [InlineData("Kedouin Makoto - Corpse Party Musume, Chapter 19 [Dametrans].zip", "Kedouin Makoto - Corpse Party Musume")]
        [InlineData("Kedouin Makoto - Corpse Party Musume, Chapter 01", "Kedouin Makoto - Corpse Party Musume")]
        [InlineData("[WS]_Ichiban_Ushiro_no_Daimaou_v02_ch10.zip", "Ichiban Ushiro no Daimaou")]
        [InlineData("[xPearse] Kyochuu Rettou Volume 1 [English] [Manga] [Volume Scans]", "Kyochuu Rettou")]
        [InlineData("Loose_Relation_Between_Wizard_and_Apprentice_c07[AN].zip", "Loose Relation Between Wizard and Apprentice")]
        [InlineData("Tower Of God S01 014 (CBT) (digital).cbz", "Tower Of God")]
        [InlineData("Tenjou_Tenge_c106[MT].zip", "Tenjou Tenge")]
        [InlineData("Tenjou_Tenge_v17_c100[MT].zip", "Tenjou Tenge")]
        [InlineData("Shimoneta - Manmaru Hen - c001-006 (v01) [Various].zip", "Shimoneta - Manmaru Hen")]
        [InlineData("Future Diary v02 (2009) (Digital) (Viz).cbz", "Future Diary")]
        [InlineData("Tonikaku Cawaii [Volume 11].cbz", "Tonikaku Cawaii")]
        [InlineData("Mujaki no Rakuen Vol12 ch76", "Mujaki no Rakuen")]
        [InlineData("Knights of Sidonia c000 (S2 LE BD Omake - BLAME!) [Habanero Scans]", "Knights of Sidonia")]
        [InlineData("Vol 1.cbz", "")]
        [InlineData("Ichinensei_ni_Nacchattara_v01_ch01_[Taruby]_v1.1.zip", "Ichinensei ni Nacchattara")]
        [InlineData("Chrno_Crusade_Dragon_Age_All_Stars[AS].zip", "")]
        [InlineData("Ichiban_Ushiro_no_Daimaou_v04_ch34_[VISCANS].zip", "Ichiban Ushiro no Daimaou")]
        [InlineData("Rent a Girlfriend v01.cbr", "Rent a Girlfriend")]
        [InlineData("Yumekui_Merry_v01_c01[Bakayarou-Kuu].rar", "Yumekui Merry")]
        public void ParseSeriesTest(string filename, string expected)
        {
            var result = Parser.ParseSeries(filename);
            Assert.Equal(expected, result);
        }
        
        [Theory]
        [InlineData("Killing Bites Vol. 0001 Ch. 0001 - Galactica Scanlations (gb)", "1")]
        [InlineData("My Girlfriend Is Shobitch v01 - ch. 09 - pg. 008.png", "9")]
        [InlineData("Historys Strongest Disciple Kenichi_v11_c90-98.zip", "90-98")]
        [InlineData("B_Gata_H_Kei_v01[SlowManga&OverloadScans]", "")]
        [InlineData("BTOOOM! v01 (2013) (Digital) (Shadowcat-Empire)", "")]
        [InlineData("Gokukoku no Brynhildr - c001-008 (v01) [TrinityBAKumA]", "1-8")]
        [InlineData("Dance in the Vampire Bund v16-17 (Digital) (NiceDragon)", "")]
        [InlineData("c001", "1")]
        [InlineData("[Suihei Kiki]_Kasumi_Otoko_no_Ko_[Taruby]_v1.12.zip", "12")]
        [InlineData("Adding volume 1 with File: Ana Satsujin Vol. 1 Ch. 5 - Manga Box (gb).cbz", "5")]
        [InlineData("Hinowa ga CRUSH! 018 (2019) (Digital) (LuCaZ).cbz", "18")]
        [InlineData("Cynthia The Mission - c000-006 (v06) [Desudesu&Brolen].zip", "0-6")]
        [InlineData("[WS]_Ichiban_Ushiro_no_Daimaou_v02_ch10.zip", "10")]
        [InlineData("Loose_Relation_Between_Wizard_and_Apprentice_c07[AN].zip", "7")]
        [InlineData("Tower Of God S01 014 (CBT) (digital).cbz", "14")]
        [InlineData("Tenjou_Tenge_c106[MT].zip", "106")]
        [InlineData("Tenjou_Tenge_v17_c100[MT].zip", "100")]
        [InlineData("Shimoneta - Manmaru Hen - c001-006 (v01) [Various].zip", "1-6")]
        [InlineData("Mujaki no Rakuen Vol12 ch76", "76")]
        [InlineData("Beelzebub_01_[Noodles].zip", "1")]
        [InlineData("Yumekui-Merry_DKThias_Chapter21.zip", "21")]
        [InlineData("Yumekui_Merry_v01_c01[Bakayarou-Kuu].rar", "1")]
        [InlineData("Yumekui-Merry_DKThias_Chapter11v2.zip", "11")]
        [InlineData("Beelzebub_53[KSH].zip", "53")]
        [InlineData("Boku no Kokoro no Yabai Yatsu Vol. 0002 Ch. 16.1 - Boredom Society (gb)", "16.1")]
        public void ParseChaptersTest(string filename, string expected)
        {
            var result = Parser.ParseChapter(filename);
            Assert.Equal(expected, result);
        }
 

        [Theory]
        [InlineData("0001", "1")]
        [InlineData("1", "1")]
        [InlineData("0013", "13")]
        public void RemoveLeadingZeroes(string input, string expected)
        {
            Assert.Equal(expected, Parser.RemoveLeadingZeroes(input));
        }

        [Theory]
        [InlineData("1", "001")]
        [InlineData("10", "010")]
        [InlineData("100", "100")]
        public void PadZeros(string input, string expected)
        {
            Assert.Equal(expected, Parser.PadZeros(input));
        }

        [Theory]
        [InlineData("Hello_I_am_here", "Hello I am here")]
        [InlineData("Hello_I_am_here   ", "Hello I am here")]
        [InlineData("[ReleaseGroup] The Title", "The Title")]
        [InlineData("[ReleaseGroup]_The_Title", "The Title")]
        public void CleanTitle(string input, string expected)
        {
            Assert.Equal(expected, Parser.CleanTitle(input));
        }

        [Theory]
        [InlineData("1", "001")]
        [InlineData("100", "100")]
        [InlineData("100.1", "100.1")]
        [InlineData("1.1", "001.1")]
        public void PerformPaddingTest(string input, string expected)
        {
            Assert.Equal(expected, Parser.PerformPadding(input));
        }
    }
}