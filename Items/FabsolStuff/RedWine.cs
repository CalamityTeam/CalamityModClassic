using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class RedWine : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Red Wine");
			/* Tooltip.SetDefault(@"Reduces life regen by 1
Too dry for my taste
Restores 200 life"); */
		}

		public override void SetDefaults()
		{
            Item.width = 28;
            Item.height = 18;
            Item.useTurn = true;
            Item.maxStack = 30;
            Item.rare = 1;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.buffType = Mod.Find<ModBuff>("RedWine").Type;
            Item.buffTime = 900;
            Item.value = Item.buyPrice(0, 2, 0, 0);
		}

        public override bool CanUseItem(Player player)
        {
            return player.FindBuffIndex(BuffID.PotionSickness) == -1;
        }

        public override bool ConsumeItem(Player player)
        {
            return player.FindBuffIndex(BuffID.PotionSickness) == -1;
        }

        public override void OnConsumeItem(Player player)
        {
            player.statLife += 200;
            if (player.statLife > player.statLifeMax2)
            {
                player.statLife = player.statLifeMax2;
            }
            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(200, true);
            }
            player.AddBuff(Mod.Find<ModBuff>("RedWine").Type, 900);
            player.AddBuff(BuffID.PotionSickness, (player.pStone ? 2700 : 3600));
        }
    }
}
