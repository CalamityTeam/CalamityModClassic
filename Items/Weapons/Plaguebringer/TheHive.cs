using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Plaguebringer
{
	public class TheHive : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("The Hive");
		}

	    public override void SetDefaults()
	    {
			Item.damage = 70;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 62;
			Item.height = 30;
			Item.useTime = 21;
			Item.useAnimation = 21;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true; //so the item's animation doesn't do damage
			Item.knockBack = 3.5f;
			Item.value = 500000;
			Item.rare = ItemRarityID.Yellow;
			Item.UseSound = SoundID.Item61;
			Item.autoReuse = true;
			Item.shoot = Mod.Find<ModProjectile>("BeeRPG").Type; //idk why but all the guns in the vanilla source have this
			Item.shootSpeed = 13f;
			Item.useAmmo = 771;
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	switch (Main.rand.Next(4))
			{
	    		case 0: type = Mod.Find<ModProjectile>("GoliathRocket").Type; break;
	    		case 1: type = Mod.Find<ModProjectile>("HiveMissile").Type; break;
	    		case 2: type = Mod.Find<ModProjectile>("HiveBomb").Type; break;
	    		case 3: type = Mod.Find<ModProjectile>("BeeRPG").Type; break;
	    		default: break;
			}
	        Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	}
}