using System;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using CalamityModClassic1Point2;

namespace CalamityModClassic1Point2.Items.DesertScourge
{
    public class AeroStone : ModItem
    {
    	public override void SetStaticDefaults()
		{
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(3, 9));
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
        	Lighting.AddLight((int)player.Center.X / 16, (int)player.Center.Y / 16, 0f, 0.425f, 0.425f);
        	player.moveSpeed += 0.1f;
        	player.jumpSpeedBoost += 2.0f;
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
