// Created by http://oleddisplay.squix.ch/ Consider a donation
// In case of problems make sure that you are using the font file with the correct version!
const uint8_t Cousine_Regular_8Bitmaps[] PROGMEM = {

	// Bitmap Data:
	0x00, // ' '
	0xA8,0x80, // '!'
	0xAA,0xA0, // '"'
	0x13,0xE5,0x1C,0x00, // '#'
	0x21,0xCE,0x0C,0xA9,0xC2,0x00, // '$'
	0xD3,0x83,0x1A,0x98, // '%'
	0x71,0x8C,0x2C,0xF8, // '&'
	0xA8, // '''
	0x0A,0x49,0x10, // '('
	0x09,0x24,0x94, // ')'
	0x4E,0xC0, // '*'
	0x21,0x3C,0x40, // '+'
	0x90, // ','
	0x40, // '-'
	0x80, // '.'
	0x10,0x88,0x44,0x20, // '/'
	0x74,0xAD,0x27,0x00, // '0'
	0xC4,0x44,0xE0, // '1'
	0xE2,0x48,0xE0, // '2'
	0xE2,0x62,0xE0, // '3'
	0x32,0x95,0xE1,0x00, // '4'
	0xCC,0x22,0xE0, // '5'
	0xEC,0xAA,0xE0, // '6'
	0xE2,0x44,0x80, // '7'
	0xEA,0xEA,0xE0, // '8'
	0xEA,0xE2,0xE0, // '9'
	0x82, // ':'
	0x40,0x28, // ';'
	0x11,0x30,0x60, // '<'
	0xF0,0x3C, // '='
	0x83,0x04,0xC0, // '>'
	0x70,0x88,0x02,0x00, // '?'
	0x72,0xAD,0xB6,0xD2,0xC7,0x00, // '@'
	0x21,0x45,0x3C,0x88, // 'A'
	0xE5,0x39,0x2E,0x00, // 'B'
	0x74,0x21,0x07,0x00, // 'C'
	0xEA,0xAA,0xE0, // 'D'
	0xE8,0xE8,0xE0, // 'E'
	0xE8,0xE8,0x80, // 'F'
	0x74,0x2D,0x27,0x00, // 'G'
	0xAA,0xEA,0xA0, // 'H'
	0xE4,0x44,0xE0, // 'I'
	0x62,0x22,0xE0, // 'J'
	0xA6,0x31,0x4A,0x00, // 'K'
	0x88,0x88,0xE0, // 'L'
	0xAA,0x62,0x20, // 'M'
	0xAA,0xEE,0xA0, // 'N'
	0x74,0xA5,0x27,0x00, // 'O'
	0xEA,0xE8,0x80, // 'P'
	0x74,0xA5,0x27,0x10,0x40, // 'Q'
	0xE5,0x39,0x4A,0x00, // 'R'
	0x72,0x0C,0x27,0x00, // 'S'
	0x70,0x82,0x08,0x20, // 'T'
	0xAA,0xAA,0xE0, // 'U'
	0x89,0x45,0x18,0x20, // 'V'
	0x8A,0xAA,0x94,0x50, // 'W'
	0x51,0x82,0x14,0x90, // 'X'
	0x51,0x42,0x08,0x20, // 'Y'
	0x70,0x82,0x10,0xF8, // 'Z'
	0xD2,0x49,0x26, // '['
	0x02,0x08,0x41,0x08, // '\'
	0xC9,0x24,0x96, // ']'
	0xCA,0xA0, // '^'
	0xF8, // '_'
	0x80, // '`'
	0xE3,0x29,0xC0, // 'a'
	0x0C,0xAA,0xAE, // 'b'
	0xE8,0x8E, // 'c'
	0x26,0xAA,0xAE, // 'd'
	0xEE,0x8E, // 'e'
	0x68,0xE8,0x88, // 'f'
	0xEA,0xAE,0xA4, // 'g'
	0x0C,0xAA,0xAA, // 'h'
	0x40,0xC4,0x4E, // 'i'
	0x20,0x62,0x22,0x2C, // 'j'
	0x88,0xAC,0xCA, // 'k'
	0xC4,0x44,0x4E, // 'l'
	0xF5,0xAD,0x60, // 'm'
	0xEA,0xAA, // 'n'
	0xEA,0xAE, // 'o'
	0xEA,0xAE,0x80, // 'p'
	0xEA,0xAE,0x22, // 'q'
	0xE8,0x88, // 'r'
	0xEC,0x2E, // 's'
	0x8C,0x88,0x60, // 't'
	0xAA,0xAE, // 'u'
	0x91,0x45,0x08, // 'v'
	0x8A,0xAF,0x14, // 'w'
	0x51,0x18,0xA0, // 'x'
	0x91,0x45,0x08,0x21,0x00, // 'y'
	0xE4,0x8E, // 'z'
	0x64,0x48,0x44,0x42, // '{'
	0xAA,0xAA, // '|'
	0x84,0x44,0x44,0x48 // '}'
};
const GFXglyph Cousine_Regular_8Glyphs[] PROGMEM = {
// bitmapOffset, width, height, xAdvance, xOffset, yOffset
	  {     0,   1,   1,   6,    0,    0 }, // ' '
	  {     1,   2,   5,   6,    2,   -5 }, // '!'
	  {     3,   4,   3,   6,    1,   -6 }, // '"'
	  {     5,   6,   5,   6,    0,   -5 }, // '#'
	  {     9,   6,   7,   6,    0,   -6 }, // '$'
	  {    15,   6,   5,   6,    0,   -5 }, // '%'
	  {    19,   6,   5,   6,    0,   -5 }, // '&'
	  {    23,   2,   3,   6,    2,   -6 }, // '''
	  {    24,   3,   8,   6,    1,   -6 }, // '('
	  {    27,   3,   8,   6,    1,   -6 }, // ')'
	  {    30,   4,   3,   6,    1,   -6 }, // '*'
	  {    32,   5,   4,   6,    0,   -5 }, // '+'
	  {    35,   3,   2,   6,    1,   -1 }, // ','
	  {    36,   4,   2,   6,    1,   -3 }, // '-'
	  {    37,   2,   1,   6,    2,   -1 }, // '.'
	  {    38,   5,   6,   6,    0,   -6 }, // '/'
	  {    42,   5,   5,   6,    0,   -5 }, // '0'
	  {    46,   4,   5,   6,    1,   -5 }, // '1'
	  {    49,   4,   5,   6,    1,   -5 }, // '2'
	  {    52,   4,   5,   6,    1,   -5 }, // '3'
	  {    55,   5,   5,   6,    0,   -5 }, // '4'
	  {    59,   4,   5,   6,    1,   -5 }, // '5'
	  {    62,   4,   5,   6,    1,   -5 }, // '6'
	  {    65,   4,   5,   6,    1,   -5 }, // '7'
	  {    68,   4,   5,   6,    1,   -5 }, // '8'
	  {    71,   4,   5,   6,    1,   -5 }, // '9'
	  {    74,   2,   4,   6,    2,   -4 }, // ':'
	  {    75,   3,   5,   6,    1,   -4 }, // ';'
	  {    77,   5,   4,   6,    0,   -5 }, // '<'
	  {    80,   5,   3,   6,    0,   -4 }, // '='
	  {    82,   5,   4,   6,    0,   -5 }, // '>'
	  {    85,   5,   5,   6,    0,   -5 }, // '?'
	  {    89,   6,   7,   6,    0,   -6 }, // '@'
	  {    95,   6,   5,   6,    0,   -5 }, // 'A'
	  {    99,   5,   5,   6,    1,   -5 }, // 'B'
	  {   103,   5,   5,   6,    0,   -5 }, // 'C'
	  {   107,   4,   5,   6,    1,   -5 }, // 'D'
	  {   110,   4,   5,   6,    1,   -5 }, // 'E'
	  {   113,   4,   5,   6,    1,   -5 }, // 'F'
	  {   116,   5,   5,   6,    0,   -5 }, // 'G'
	  {   120,   4,   5,   6,    1,   -5 }, // 'H'
	  {   123,   4,   5,   6,    1,   -5 }, // 'I'
	  {   126,   4,   5,   6,    1,   -5 }, // 'J'
	  {   129,   5,   5,   6,    1,   -5 }, // 'K'
	  {   133,   4,   5,   6,    1,   -5 }, // 'L'
	  {   136,   4,   5,   6,    1,   -5 }, // 'M'
	  {   139,   4,   5,   6,    1,   -5 }, // 'N'
	  {   142,   5,   5,   6,    0,   -5 }, // 'O'
	  {   146,   4,   5,   6,    1,   -5 }, // 'P'
	  {   149,   5,   7,   6,    0,   -5 }, // 'Q'
	  {   154,   5,   5,   6,    1,   -5 }, // 'R'
	  {   158,   5,   5,   6,    0,   -5 }, // 'S'
	  {   162,   6,   5,   6,    0,   -5 }, // 'T'
	  {   166,   4,   5,   6,    1,   -5 }, // 'U'
	  {   169,   6,   5,   6,    0,   -5 }, // 'V'
	  {   173,   6,   5,   6,    0,   -5 }, // 'W'
	  {   177,   6,   5,   6,    0,   -5 }, // 'X'
	  {   181,   6,   5,   6,    0,   -5 }, // 'Y'
	  {   185,   6,   5,   6,    0,   -5 }, // 'Z'
	  {   189,   3,   8,   6,    2,   -6 }, // '['
	  {   192,   5,   6,   6,    0,   -6 }, // '\'
	  {   196,   3,   8,   6,    1,   -6 }, // ']'
	  {   199,   4,   3,   6,    1,   -5 }, // '^'
	  {   201,   6,   1,   6,    0,    0 }, // '_'
	  {   202,   2,   1,   6,    2,   -6 }, // '`'
	  {   203,   5,   4,   6,    1,   -4 }, // 'a'
	  {   206,   4,   6,   6,    1,   -6 }, // 'b'
	  {   209,   4,   4,   6,    1,   -4 }, // 'c'
	  {   211,   4,   6,   6,    1,   -6 }, // 'd'
	  {   214,   4,   4,   6,    1,   -4 }, // 'e'
	  {   216,   4,   6,   6,    1,   -6 }, // 'f'
	  {   219,   4,   6,   6,    1,   -4 }, // 'g'
	  {   222,   4,   6,   6,    1,   -6 }, // 'h'
	  {   225,   4,   6,   6,    1,   -6 }, // 'i'
	  {   228,   4,   8,   6,    0,   -6 }, // 'j'
	  {   232,   4,   6,   6,    1,   -6 }, // 'k'
	  {   235,   4,   6,   6,    1,   -6 }, // 'l'
	  {   238,   5,   4,   6,    0,   -4 }, // 'm'
	  {   241,   4,   4,   6,    1,   -4 }, // 'n'
	  {   243,   4,   4,   6,    1,   -4 }, // 'o'
	  {   245,   4,   6,   6,    1,   -4 }, // 'p'
	  {   248,   4,   6,   6,    1,   -4 }, // 'q'
	  {   251,   4,   4,   6,    1,   -4 }, // 'r'
	  {   253,   4,   4,   6,    1,   -4 }, // 's'
	  {   255,   4,   5,   6,    1,   -5 }, // 't'
	  {   258,   4,   4,   6,    1,   -4 }, // 'u'
	  {   260,   6,   4,   6,    0,   -4 }, // 'v'
	  {   263,   6,   4,   6,    0,   -4 }, // 'w'
	  {   266,   5,   4,   6,    0,   -4 }, // 'x'
	  {   269,   6,   6,   6,    0,   -4 }, // 'y'
	  {   274,   4,   4,   6,    1,   -4 }, // 'z'
	  {   276,   4,   8,   6,    1,   -6 }, // '{'
	  {   280,   2,   8,   6,    2,   -6 }, // '|'
	  {   282,   4,   8,   6,    1,   -6 } // '}'
};
const GFXfont Cousine_Regular_8 PROGMEM = {
(uint8_t  *)Cousine_Regular_8Bitmaps,(GFXglyph *)Cousine_Regular_8Glyphs,0x20, 0x7E, 10};