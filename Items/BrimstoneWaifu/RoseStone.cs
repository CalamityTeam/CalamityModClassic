using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.BrimstoneWaifu
{
    public class RoseStone : ModItem
    {
    	public override void SetStaticDefaults()
	 	{
	 		Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 11));
	 	}
    	
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 500000;
            Item.rare = ItemRarityID.Pink;
			Item.accessory = true;
        }
        
        public override void UpdateAccessory(Player player, bool hideVisual)
		{
        	Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.6f, 0f, 0.25f);
			player.lifeRegen += 2;
			player.statLifeMax2 += 50;
			player.GetCritChance(DamageClass.Melee) += 2;
			player.GetDamage(DamageClass.Melee) += 0.02f;
			player.GetCritChance(DamageClass.Magic) += 2;
			player.GetDamage(DamageClass.Magic) += 0.02f;
			player.GetCritChance(DamageClass.Ranged) += 2;
			player.GetDamage(DamageClass.Ranged) += 0.02f;
			player.GetCritChance(DamageClass.Throwing) += 2;
			player.GetDamage(DamageClass.Throwing) += 0.02f;
			player.GetDamage(DamageClass.Summon) += 0.02f;
			CalamityPlayer1Point2 modPlayer = player.GetModPlayer<CalamityPlayer1Point2>();
			modPlayer.brimstoneWaifu = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("BrimstoneWaifu").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("BrimstoneWaifu").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("BigBustyRose").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("BigBustyRose").Type, 60, 2f, Main.myPlayer, 0f, 0f);
				}
			}
		}
    }
}
