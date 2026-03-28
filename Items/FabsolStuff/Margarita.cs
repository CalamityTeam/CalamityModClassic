using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassicPreTrailer.Items.FabsolStuff
{
	public class Margarita : ModItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Margarita");
			/* Tooltip.SetDefault(@"Makes you immune to most debuffs
Reduces defense by 6 and life regen by 1
One of the best drinks ever created, enjoy it while it lasts
Restores 200 life and mana"); */
		}

		public override void SetDefaults()
		{
            Item.width = 28;
            Item.height = 18;
            Item.useTurn = true;
            Item.maxStack = 30;
            Item.rare = 5;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = 2;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.buffType = Mod.Find<ModBuff>("Margarita").Type;
            Item.buffTime = 10800; //3 minutes
            Item.value = Item.buyPrice(0, 23, 30, 0);
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
            player.statMana += 200;
            if (player.statLife > player.statLifeMax2)
            {
                player.statLife = player.statLifeMax2;
            }
            if (player.statMana > player.statManaMax2)
            {
                player.statMana = player.statManaMax2;
            }
            player.AddBuff(BuffID.ManaSickness, Player.manaSickTime, true);
            if (Main.myPlayer == player.whoAmI)
            {
                player.HealEffect(200, true);
                player.ManaEffect(200);
            }
            player.AddBuff(Mod.Find<ModBuff>("Margarita").Type, 10800);
            player.AddBuff(BuffID.PotionSickness, (player.pStone ? 2700 : 3600));
        }
    }
}
