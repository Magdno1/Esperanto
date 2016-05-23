using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esperanto
{
    public enum GameLanguage { Japanese, English }

    public class StringHandler
    {
        static readonly char[] charaMapBaseJapanese = new char[]
        {
            '　', '０', '１', '２', '３', '４', '５', '６', '７', '８', '９', 'Ａ', 'Ｂ', 'Ｃ', 'Ｄ', 'Ｅ',
            'Ｆ', 'Ｇ', 'Ｈ', 'Ｉ', 'Ｊ', 'Ｋ', 'Ｌ', 'Ｍ', 'Ｎ', 'Ｏ', 'Ｐ', 'Ｑ', 'Ｒ', 'Ｓ', 'Ｔ', 'Ｕ',
            'Ｖ', 'Ｗ', 'Ｘ', 'Ｙ', 'Ｚ', 'ａ', 'ｂ', 'ｃ', 'ｄ', 'ｅ', 'ｆ', 'ｇ', 'ｈ', 'ｉ', 'ｊ', 'ｋ',
            'ｌ', 'ｍ', 'ｎ', 'ｏ', 'ｐ', 'ｑ', 'ｒ', 'ｓ', 'ｔ', 'ｕ', 'ｖ', 'ｗ', 'ｘ', 'ｙ', 'ｚ', 'ー',
            'ぁ', 'あ', 'ぃ', 'い', 'ぅ', 'う', 'ぇ', 'え', 'ぉ', 'お', 'か', 'が', 'き', 'ぎ', 'く', 'ぐ',
            'け', 'げ', 'こ', 'ご', 'さ', 'ざ', 'し', 'じ', 'す', 'ず', 'せ', 'ぜ', 'そ', 'ぞ', 'た', 'だ',
            'ち', 'ぢ', 'っ', 'つ', 'づ', 'て', 'で', 'と', 'ど', 'な', 'に', 'ぬ', 'ね', 'の', 'は', 'ば',
            'ぱ', 'ひ', 'び', 'ぴ', 'ふ', 'ぶ', 'ぷ', 'へ', 'べ', 'ぺ', 'ほ', 'ぼ', 'ぽ', 'ま', 'み', 'む',
            'め', 'も', 'ゃ', 'や', 'ゅ', 'ゆ', 'ょ', 'よ', 'ら', 'り', 'る', 'れ', 'ろ', 'ゎ', 'わ', 'を',
            'ん', 'ァ', 'ア', 'ィ', 'イ', 'ゥ', 'ウ', 'ェ', 'エ', 'ォ', 'オ', 'カ', 'ガ', 'キ', 'ギ', 'ク',
            'グ', 'ケ', 'ゲ', 'コ', 'ゴ', 'サ', 'ザ', 'シ', 'ジ', 'ス', 'ズ', 'セ', 'ゼ', 'ソ', 'ゾ', 'タ',
            'ダ', 'チ', 'ヂ', 'ッ', 'ツ', 'ヅ', 'テ', 'デ', 'ト', 'ド', 'ナ', 'ニ', 'ヌ', 'ネ', 'ノ', 'ハ',
            'バ', 'パ', 'ヒ', 'ビ', 'ピ', 'フ', 'ブ', 'プ', 'ヘ', 'ベ', 'ペ', 'ホ', 'ボ', 'ポ', 'マ', 'ミ',
            'ム', 'メ', 'モ', 'ャ', 'ヤ', 'ュ', 'ユ', 'ョ', 'ヨ', 'ラ', 'リ', 'ル', 'レ', 'ロ', 'ヮ', 'ワ',
            'ヲ', 'ン', 'ヴ', 'ヵ', 'ヶ', '！', '△', '▲', '→', '％', '＆', '’', '（', '）', '＊', '＋'
        };

        static readonly char[] charaMapBaseEnglish = new char[]
        {
            ' ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E',
            'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
            'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
            'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'ー',
            'ぁ', 'あ', 'ぃ', 'い', 'ぅ', 'う', 'ぇ', 'え', 'ぉ', 'お', 'か', 'が', 'き', 'ぎ', 'く', 'ぐ',
            'け', 'げ', 'こ', 'ご', 'さ', 'ざ', 'し', 'じ', 'す', 'ず', 'せ', 'ぜ', 'そ', 'ぞ', 'た', 'だ',
            'ち', 'ぢ', 'っ', 'つ', 'づ', 'て', 'で', 'と', 'ど', 'な', 'に', 'ぬ', 'ね', 'の', 'は', 'ば',
            'ぱ', 'ひ', 'び', 'ぴ', 'ふ', 'ぶ', 'ぷ', 'へ', 'べ', 'ぺ', 'ほ', 'ぼ', 'ぽ', 'ま', 'み', 'む',
            'め', 'も', 'ゃ', 'や', 'ゅ', 'ゆ', 'ょ', 'よ', 'ら', 'り', 'る', 'れ', 'ろ', 'ゎ', 'わ', 'を',
            'ん', 'ァ', 'ア', 'ィ', 'イ', 'ゥ', 'ウ', 'ェ', 'エ', 'ォ', 'オ', 'カ', 'ガ', 'キ', 'ギ', 'ク',
            'グ', 'ケ', 'ゲ', 'コ', 'ゴ', 'サ', 'ザ', 'シ', 'ジ', 'ス', 'ズ', 'セ', 'ゼ', 'ソ', 'ゾ', 'タ',
            'ダ', 'チ', 'ヂ', 'ッ', 'ツ', 'ヅ', 'テ', 'デ', 'ト', 'ド', 'ナ', 'ニ', 'ヌ', 'ネ', 'ノ', 'ハ',
            'バ', 'パ', 'ヒ', 'ビ', 'ピ', 'フ', 'ブ', 'プ', 'ヘ', 'ベ', 'ペ', 'ホ', 'ボ', 'ポ', 'マ', 'ミ',
            'ム', 'メ', 'モ', 'ャ', 'ヤ', 'ュ', 'ユ', 'ョ', 'ヨ', 'ラ', 'リ', 'ル', 'レ', 'ロ', 'ヮ', 'ワ',
            'ヲ', 'ン', 'ヴ', 'ヵ', 'ヶ', '!', '△', '▲', '→', '%', '&', '\'', '(', ')', '*', '+'
        };

        static readonly char[] charaMapF0Japanese = new char[]
        {
            /*         00    01    02    03    04    05   06    07    08    09    0A    0B    0C    0D    0E    0F*/
            /* 0x00 */ '、', '―', '。', '／', '：', '＜', '＝', '＞', '？', '［', '］', '＿', '～', '「', '」', '　',
            /* 0x10 */ '思', '気', '力', '人', '間', '機', '械', '本', '当', '年', '月', '日', '伝', '説', '私', '〇',
            /* 0x20 */ '自', '由', '平', '和', '〇', '存', '〇', '不', '安', '聞', '言', '知', '成', '功', '失', '敗',
            /* 0x30 */ '理', '想', '郷', '新', '古', '旧', '全', '滅', '他', '最', '近', '遠', '〇', '方', '敵', '助',
            /* 0x40 */ '未', '来', '過', '去', '生', '死', '科', '学', '同', '点', '口', '目', '大', '〇', '感', '地',
            /* 0x50 */ '終', '長', '動', '止', '〇', '右', '左', '上', '下', '時', '列', '車', '都', '市', '転', '送',
            /* 0x60 */ '工', '場', '信', '破', '壊', '高', '〇', '多', '少', '防', '御', '攻', '撃', '真', '回', '路',
            /* 0x70 */ '所', '在', '軍', '彼', '〇', '出', '入', '街', '声', '必', '鉄', '型', '廃', '砂', '漠', '爆',
            /* 0x80 */ '発', '底', '塔', '〇', '前', '後', '〇', '守', '現', '会', '基', '誰', '士', '作', '品', '団',
            /* 0x90 */ '事', '無', '神', '聖', '域', '脱', '主', '救', '世', '戦', '〇', '手', '捕', '実', '〇', '涙',
            /* 0xA0 */ '名', '〇', '〇', '四', '天', '王', '赤', '青', '〇', '〇', '頼', '者', '内', '外', '〇', '〇',
            /* 0xB0 */ '強', '弱', '数', '使', '用', '悪', '〇', '何', '〇', '呼', '以', '再', '々', '〇', '‥', '英',
            /* 0xC0 */ '雄', '見', '消', '〇', '度', '女', '行', '分', '部', '形', '話', '体', '倍', '巨', '侵', '活',
            /* 0xD0 */ '続', '界', '永', '処', '仲', '中', '隊', '室', '心', '明', '情', '報', '収', '集', '利', '向',
            /* 0xE0 */ '〇', '配', '〇', '闘', '〇', '〇', '〇', '今', '系', '　', '★', '☆', '　', '“', '・', 'Σ',
        };

        static readonly char[] charaMapF0English = new char[]
        {
            /*         00    01    02    03    04    05   06    07    08    09    0A    0B    0C    0D    0E    0F*/
            /* 0x00 */ '、', '-', '。', '/', ':', '＜', '=', '＞', '?', '[', ']', '_', '~', '「', '」', ' ',
            /* 0x10 */ '思', '気', '力', '人', '間', '機', '械', '本', '当', '年', '月', '日', '伝', '説', '私', '〇',
            /* 0x20 */ '自', '由', '平', '和', '〇', '存', '〇', '不', '安', '聞', '言', '知', '成', '功', '失', '敗',
            /* 0x30 */ '理', '想', '郷', '新', '古', '旧', '全', '滅', '他', '最', '近', '遠', '〇', '方', '敵', '助',
            /* 0x40 */ '未', '来', '過', '去', '生', '死', '科', '学', '同', '点', '口', '目', '大', '〇', '感', '地',
            /* 0x50 */ '終', '長', '動', '止', '〇', '右', '左', '上', '下', '時', '列', '車', '都', '市', '転', '送',
            /* 0x60 */ '工', '場', '信', '破', '壊', '高', '〇', '多', '少', '防', '御', '攻', '撃', '真', '回', '路',
            /* 0x70 */ '所', '在', '軍', '彼', '〇', '出', '入', '街', '声', '必', '鉄', '型', '廃', '砂', '漠', '爆',
            /* 0x80 */ '発', '底', '塔', '〇', '前', '後', '〇', '守', '現', '会', '基', '誰', '士', '作', '品', '団',
            /* 0x90 */ '事', '無', '神', '聖', '域', '脱', '主', '救', '世', '戦', '〇', '手', '捕', '実', '〇', '涙',
            /* 0xA0 */ '名', '〇', '〇', '四', '天', '王', '赤', '青', '〇', '〇', '頼', '者', '内', '外', '〇', '〇',
            /* 0xB0 */ '強', '弱', '数', '使', '用', '悪', '〇', '何', '〇', '呼', '以', '再', '々', '〇', '‥', '英',
            /* 0xC0 */ '雄', '見', '消', '〇', '度', '女', '行', '分', '部', '形', '話', '体', '倍', '巨', '侵', '活',
            /* 0xD0 */ '続', '界', '永', '処', '仲', '中', '隊', '室', '心', '明', '情', '報', '収', '集', '利', '向',
            /* 0xE0 */ '〇', '配', '〇', '闘', '〇', '〇', '〇', '今', '系', '.', '★', '☆', ',', '"', '・', 'Σ'
        };

        static Dictionary<byte, string> boxStyleNames = new Dictionary<byte, string>()
        {
            { 0x00, "SmallBoxLeft" },
            { 0x01, "SmallBoxRight" },
            { 0x02, "LargeLeft" },
            { 0x03, "LargeRight" },
            { 0x04, "ZeroLeft" },
            { 0x05, "ZeroRight" },
            { 0x06, "CielLeft" },
            { 0x07, "CielRight" },
            { 0x08, "MilanLeft" },
            { 0x09, "MilanRight" },
            { 0x0A, "PassyLeft" },
            { 0x0B, "PassyRight" },
            { 0x0C, "PantheonLeft" },
            { 0x0D, "PantheonRight" },
            { 0x0E, "XLeft" },
            { 0x0F, "XRight" },

            { 0x10, "AnubisLeft" },
            { 0x11, "AnubisRight" },
            { 0x12, "AztecLeft" },
            { 0x13, "AztecRight" },
            { 0x14, "HanumachineLeft" },
            { 0x15, "HanumachineRight" },
            { 0x16, "BlizzackLeft" },
            { 0x17, "BlizzackRight" },
            { 0x18, "MahaLeft" },
            { 0x19, "MahaRight" },
            { 0x1A, "HerculiousLeft" },
            { 0x1B, "HerculiousRight" },
            { 0x1C, "CopyXArmorLeft" },
            { 0x1D, "CopyXArmorRight" },
            { 0x1E, "LeviathanLeft" },
            { 0x1F, "LeviathanRight" },

            { 0x20, "PhantomLeft" },
            { 0x21, "PhantomRight" },
            { 0x22, "HarpuiaLeft" },
            { 0x23, "HarpuiaRight" },
            { 0x24, "FefnirLeft" },
            { 0x25, "FefnirRight" },
            { 0x26, "CopyXLeft" },
            { 0x27, "CopyXRight" },
            { 0x28, "CopyXBrokenLeft" },
            { 0x29, "CopyXBrokenRight" },
            { 0x2A, "SoundOnlyLeft" },
            { 0x2B, "SoundOnlyRight" },
            { 0x2C, "ResistanceMaleLeft" },
            { 0x2D, "ResistanceMaleRight" },
            { 0x2E, "ResistanceFemaleLeft" },
            { 0x2F, "ResistanceFemaleRight" },

            { 0x30, "DandeLeft" },
            { 0x31, "DandeRight" },
            { 0x32, "CerveauLeft" },
            { 0x33, "CerveauRight" },
            { 0x34, "AlouetteLeft" },
            { 0x35, "AlouetteRight" },
            { 0x36, "AndrewLeft" },
            { 0x37, "AndrewRight" },
            { 0x38, "ResistanceLeft" },
            { 0x39, "ResistanceRight" },
            { 0x3A, "StaticLeft" },
            { 0x3B, "StaticRight" },
            { 0x3C, "HibouLeft" },
            { 0x3D, "HibouRight" }
        };

        public static string GetString(byte[] bytes, GameLanguage language)
        {
            return GetString(bytes, 0, bytes.Length, language);
        }

        public static string GetString(byte[] bytes, int startIndex, GameLanguage language)
        {
            int length = 0;
            for (int i = startIndex; i < bytes.Length; i++)
            {
                length++;
                if (bytes[i] == 0xFF) break;
            }
            return GetString(bytes, startIndex, length, language);
        }

        public static string GetString(byte[] bytes, int startIndex, int length, GameLanguage language)
        {
            char[] charaMapBase = (language == GameLanguage.Japanese ? charaMapBaseJapanese : charaMapBaseEnglish);
            char[] charaMapF0 = (language == GameLanguage.Japanese ? charaMapF0Japanese : charaMapF0English);

            StringBuilder builder = new StringBuilder();

            for (int i = startIndex; ((i < startIndex + length) && (i < bytes.Length)); i++)
            {
                byte value = bytes[i];

                if (value < charaMapBase.Length)
                    builder.Append(charaMapBase[value]);
                else if (value == 0xF0)
                {
                    char ch = charaMapF0[bytes[i + 1]];
                    if (ch == '〇')
                        builder.AppendFormat("[Ext:{0:X2}]", bytes[i + 1]); // TODO: finish the damn kanji!
                    else
                        builder.Append(ch);
                    i++;
                }
                else
                {
                    switch (value)
                    {
                        case 0xF1: builder.Append("[Highlight:Off]"); break;
                        case 0xF2: builder.Append("[Highlight:On]"); break;
                        case 0xF3:
                            if (boxStyleNames.ContainsKey(bytes[i + 1]))
                                builder.AppendFormat("[BoxStyle:{0}]{1}", boxStyleNames[bytes[i + 1]], Environment.NewLine);
                            else
                                builder.AppendFormat("[BoxStyle:0x{0:X2}]{1}", bytes[i + 1], Environment.NewLine);
                            i++;
                            break;
                        case 0xF4: builder.AppendFormat("[Branch:0x{0:X2}]{1}", bytes[i + 1], Environment.NewLine); i++; break;
                        case 0xF6: builder.AppendFormat("[Choice:0x{0:X2}]{1}", bytes[i + 1], Environment.NewLine); i++; break;
                        case 0xF8: builder.AppendFormat("[Unknown:0x{0:X2}]", bytes[i + 1]); i++; break;
                        case 0xFB: builder.Append("[UnknownFB]"); break;
                        case 0xFC: builder.AppendFormat("{0}", Environment.NewLine); break;
                        case 0xFD: builder.AppendFormat("{0}{0}", Environment.NewLine); break;
                        case 0xFE: builder.AppendFormat("{0}[Break]{0}{0}", Environment.NewLine); break;
                        case 0xFF: builder.AppendFormat("{0}[End]{0}", Environment.NewLine); return builder.ToString();
                    }
                }
            }

            return builder.ToString();
        }
    }
}
