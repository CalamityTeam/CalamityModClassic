using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items
{
	public class RomajedaOrchid : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Romajeda Orchid");
			// Tooltip.SetDefault("Summons a never forgotten friend");
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
            Item.shoot = Mod.Find<ModProjectile>("Kendra").Type;
            Item.buffType = Mod.Find<ModBuff>("Kendra").Type;
			Item.rare = 5;
			Item.UseSound = SoundID.Item44;
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
