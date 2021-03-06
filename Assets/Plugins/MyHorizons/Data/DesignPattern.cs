﻿using System;
using System.IO;
using MyHorizons.Data.PlayerData;
using MyHorizons.Data.Save;

namespace MyHorizons.Data
{
    public class DesignPattern
    {
        public string Name;
        public PersonalID PersonalID;
        public int Index;
        public readonly DesignColor[] Palette = new DesignColor[15];
        public byte[] Pixels;
		public bool IsPro;

        private readonly int Offset;
		public TypeEnum Type;
		public enum TypeEnum
		{
			SimplePattern = 0x00,
			EmptyProPattern = 0x01,
			SimpleShirt = 0x02,
			LongSleeveShirt = 0x03,
			TShirt = 0x04,
			Tanktop = 0x05,
			Pullover = 0x06,
			Coat = 0x07,
			Hoodie = 0x08,
			ShortSleeveDress = 0x09,
			SleevelessDress = 0x0A,
			LongSleeveDress = 0x0B,
			BalloonDress = 0x0C,
			RoundDress = 0x0D,
			Robe = 0x0E,
			BrimmedCap = 0x0F,
			KnitCap = 0x10,
			BrimmedHat = 0x11,
			ShortSleeveDress3DS = 0x12,
			LongSleeveDress3DS = 0x13,
			NoSleeveDress3DS = 0x14,
			ShortSleeveShirt3DS = 0x15,
			LongSleeveShirt3DS = 0x16,
			NoSleeveShirt3DS = 0x17,
			Hat3DS = 0x18,
			HornHat3DS = 0x19,
			Unsupported = 0xFF
		}

		public byte Width
		{
			get
			{
				return (byte) (this.Type == TypeEnum.SimplePattern ? 32 : 64);
			}
		}

		public byte Height
		{
			get
			{
				return (byte) (this.Type == TypeEnum.SimplePattern ? 32 : 64);
			}
		}

		public class DesignColor
        {
			public DesignColor()
			{

			}

            public DesignColor(int offset)
            {
                var save = MainSaveFile.Singleton();
                R = save.ReadU8(offset + 0);
                G = save.ReadU8(offset + 1);
                B = save.ReadU8(offset + 2);
            }

            public byte R;
            public byte G;
            public byte B;
        }

        private readonly struct Offsets
        {
            public readonly int BaseOffset;
			public readonly int Size;

            public readonly int Name;
            public readonly int PersonalID;
            public readonly int Palette;
            public readonly int Image;
			public readonly int Type;

            public Offsets(int baseOffset, int size, int type, int name, int personalID, int palette, int image)
            {
				Type = type;
                BaseOffset = baseOffset;
                Size = size;
                Name = name;
                PersonalID = personalID;
                Palette = palette;
                Image = image;
            }
        }

		private static readonly Offsets[] DesignPatternOffsetsByRevision =
		{
			new Offsets(0x1D72F0, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5), // not entirely sure
            new Offsets(0x1D7310, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x1D7310, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x1D7310, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x1D7310, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x1D7310, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x1D7310, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x2A8, 0x2A5, 0x10, 0x38, 0x78, 0xA5), // designs files
		};

		private static readonly Offsets[] ProDesignPatternOffsetsByRevision =
		{
			new Offsets(0x1DF7C0, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5), // not entirely sure
            new Offsets(0x1DF7E0, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x1DF7E0, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x1DF7E0, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x1DF7E0, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x1DF7E0, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x1DF7E0, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x000000, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5),
			new Offsets(0x00D480, 0x8A8, 0x8A5, 0x10, 0x38, 0x78, 0xA5), // designs files
		};

		private static Offsets GetOffsetsFromRevision() => DesignPatternOffsetsByRevision[MainSaveFile.Singleton().GetRevision()];
		private static Offsets GetProDesignOffsetsFromRevision() => ProDesignPatternOffsetsByRevision[MainSaveFile.Singleton().GetRevision()];

		public DesignPattern()
		{
			for (int i = 0; i < 15; i++)
				Palette[i] = new DesignColor();
		}

        public DesignPattern(int idx, bool proDesign = false)
        {
			IsPro = proDesign;
            Index = idx;
            var save = MainSaveFile.Singleton();
            var offsets = proDesign ? GetProDesignOffsetsFromRevision() : GetOffsetsFromRevision();
            Offset = offsets.BaseOffset + idx * offsets.Size;

			Type = (TypeEnum) save.ReadU8(Offset + offsets.Type);
            Name = save.ReadString(Offset + offsets.Name, 20);
            PersonalID = save.ReadStruct<PersonalID>(Offset + offsets.PersonalID);

            for (int i = 0; i < 15; i++)
                Palette[i] = new DesignColor(Offset + offsets.Palette + i * 3);

			if (this.Type == TypeEnum.SimplePattern)
				this.Pixels = save.ReadArray<byte>(Offset + offsets.Image, (this.Width / 2) * this.Height);
			else
			{
				// create one big picture
				var pixels = save.ReadArray<byte>(Offset + offsets.Image, (this.Width / 2) * this.Height);
				this.Pixels = new byte[this.Width / 2 * this.Height];
				for (int y = 0; y < this.Height; y++)
				{
					for (int x = 0; x < this.Width / 2; x++)
					{
						var offset = (x >= this.Width / 4 ? 0x200 : 0x0) + (y >= this.Height / 2 ? 0x400 : 0x0);
						this.Pixels[x + y * this.Width / 2] = pixels[offset + x % (this.Width / 4) +  (y % (this.Height / 2)) * (this.Width / 4)];
					}
				}
			}
        }

		public byte GetPixel(int x, int y)
        {
            if (x < 0 || x >= Width)
                throw new ArgumentException("Argument out of range (0-"+Width+")", "x");
            if (y < 0 || y >= Height)
                throw new ArgumentException("Argument out of range (0-"+Height+")", "y");

            if (x % 2 == 0)
                return (byte) (Pixels[(x / 2) + y * (Width / 2)] & 0x0F);
            else
                return (byte) ((Pixels[(x / 2) + y * (Width / 2)] & 0xF0) >> 4);
        }

        public void SetPixel(int x, int y, byte paletteColorIndex)
        {
            if (x < 0 || x >= Width)
                throw new ArgumentException("Argument out of range (0-"+Width+")", "x");
            if (y < 0 || y >= Height)
                throw new ArgumentException("Argument out of range (0-"+Height+")", "y");
            if (paletteColorIndex > 15)
                throw new ArgumentException("Argument out of range (0-15)", "paletteColorIndex");

            var index = (x / 2) + y * (Width / 2);
            if (x % 2 == 0)
                Pixels[index] = (byte) ((paletteColorIndex & 0x0F) | Pixels[index] & 0xF0);
            else
                Pixels[index] = (byte) (((paletteColorIndex * 0x10) & 0xF0) | Pixels[index] & 0x0F);
        }

        public void Save()
        {
            var save = MainSaveFile.Singleton();
			var offsets = this.IsPro ? GetProDesignOffsetsFromRevision() : GetOffsetsFromRevision();

			save.WriteString(Offset + offsets.Name, this.Name, 20);
            save.WriteStruct<PersonalID>(Offset + offsets.PersonalID, this.PersonalID);

            for (int i = 0; i < 15; i++)
            {
                save.WriteU8(Offset + offsets.Palette + i * 3 + 0, this.Palette[i].R);
                save.WriteU8(Offset + offsets.Palette + i * 3 + 1, this.Palette[i].G);
                save.WriteU8(Offset + offsets.Palette + i * 3 + 2, this.Palette[i].B);
            }

			if (this.Type == TypeEnum.SimplePattern)
			{
				save.WriteArray<byte>(Offset + offsets.Image, this.Pixels);
			}
			else
			{
				var pixels = new byte[this.Width / 2 * this.Height];
				for (int y = 0; y < this.Height; y++)
				{
					for (int x = 0; x < this.Width / 2; x++)
					{

						/*var offset = (x > this.Width / 4 ? 0x200 : 0x0) + (y > this.Height / 2 ? 0x400 : 0x0);
						this.Pixels[x + y * this.Width / 2] = pixels[offset + x % (this.Width / 4) + (y % (this.Height / 2)) * (this.Width / 4)];*/


						var offset = (x >= this.Width / 4 ? 0x200 : 0x0) + (y >= this.Height / 2 ? 0x400 : 0x0);
						pixels[offset + x % (this.Width / 4) + (y % (this.Height / 2)) * (this.Width / 4)] = this.Pixels[x + y * this.Width / 2];
					}
				}
				save.WriteArray<byte>(Offset + offsets.Image, pixels);
				//save.WriteU8(Offset + offsets.Type, (byte) this.Type);
				save.WriteU8(Offset + offsets.Type, (byte) this.Type);
			}
		}
    }
}
