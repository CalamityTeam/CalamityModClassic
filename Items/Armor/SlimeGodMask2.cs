using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.Armor
{
	[AutoloadEquip(EquipType.Head)]
	public class SlimeGodMask2 : ModItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Slime God Mask");
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }

		public override void SetDefaults()
		{
			Item.width = 28;
			Item.height = 20;
			Item.rare = 1;
			Item.vanity = true;
		}
	}
}