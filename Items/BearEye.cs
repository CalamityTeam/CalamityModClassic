using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items
{
	public class BearEye : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Bear's Eye");
			// Tooltip.SetDefault("Summons a pet guardian angel");
		}
		public override void SetDefaults()
		{
            Item.damage = 0;
			Item.useStyle = 1;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.noMelee = true;
			Item.width = 30;
            Item.height = 30;
            Item.value = Item.sellPrice(1, 0, 0, 0);
            Item.shoot = Mod.Find<ModProjectile>("Bear").Type;
            Item.buffType = Mod.Find<ModBuff>("BearBuff").Type;
			Item.rare = 5;
			Item.UseSound = SoundID.Meowmere;
		}

        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(Item.buffType, 15, true);
            }
        }
	}
}
