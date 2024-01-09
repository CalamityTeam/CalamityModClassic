using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityModClassic1Point2.Items.Weapons 
{
	public class LunicEye : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Lunic Eye");
			//Tooltip.SetDefault("Fires lunic beams that reduce enemy protection\nHolding this weapon reduces stress\nProjectile damage is multiplied by all of your damage bonuses");
		}

		public override void SetDefaults()
		{
			Item.width = 80;
			Item.damage = 18;
			Item.rare = ItemRarityID.Pink;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 4.5f;
			Item.UseSound = new Terraria.Audio.SoundStyle("CalamityModClassic1Point1/Sounds/Item/LaserCannon");
			Item.autoReuse = true;
			Item.noMelee = true;
			Item.height = 50;
			Item.value = 100000;
			Item.shoot = Mod.Find<ModProjectile>("LunicBeam").Type;
			Item.shootSpeed = 13f;
		}
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-15, 0);
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
			float damageMult = player.GetDamage(DamageClass.Melee).Flat + player.GetDamage(DamageClass.Ranged).Flat + player.GetDamage(DamageClass.Magic).Flat + player.GetDamage(DamageClass.Throwing).Flat + player.GetDamage(DamageClass.Summon).Flat;
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, type, (int)((double)damage * damageMult), knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(null, "Stardust", 20);
			recipe.AddIngredient(null, "AerialiteBar", 15);
			recipe.AddIngredient(ItemID.SunplateBlock, 15);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
		}
	}
}
