using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Calamitas
{
    public class ChaosStone : ModItem
    {
    	public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 4));
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
        	Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0.85f, 0f, 0f);
			player.statManaMax2 += 50;
			player.manaCost *= 0.95f;
			player.GetCritChance(DamageClass.Melee) += 2;
			player.GetDamage(DamageClass.Melee) += 0.02f;
			player.GetCritChance(DamageClass.Magic) += 2;
			player.GetDamage(DamageClass.Magic) += 0.02f;
			player.GetCritChance(DamageClass.Ranged) += 2;
			player.GetDamage(DamageClass.Ranged) += 0.02f;
			player.GetCritChance(DamageClass.Throwing) += 2;
			player.GetDamage(DamageClass.Throwing) += 0.02f;
			player.GetDamage(DamageClass.Summon) += 0.02f;
		}
    }
}
