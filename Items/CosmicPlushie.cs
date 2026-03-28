using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items
{
	public class CosmicPlushie : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cosmic Plushie");
			// Tooltip.SetDefault("Summons the devourer of the cosmos...?\nSharp objects possibly included\nSuppresses friendly red devils");
		}
		public override void SetDefaults()
		{
            Item.damage = 0;
			Item.useStyle = 1;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.noMelee = true;
			Item.width = 28;
            Item.height = 36;
            Item.value = Item.sellPrice(0, 7, 0, 0);
            Item.shoot = Mod.Find<ModProjectile>("ChibiiDoggo").Type;
            Item.buffType = Mod.Find<ModBuff>("ChibiiBuff").Type;
			Item.rare = 10;
			Item.UseSound = SoundID.Meowmere;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
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
