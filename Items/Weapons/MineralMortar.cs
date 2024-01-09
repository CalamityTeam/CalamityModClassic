using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons
{
	public class MineralMortar : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Mineral Mortar");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 41;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 58;
	        Item.height = 26;
	        Item.useTime = 33;
	        Item.useAnimation = 33;
	        Item.useStyle = ItemUseStyleID.Shoot;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
	        Item.value = 300000;
	        Item.rare = ItemRarityID.Pink;
	        Item.UseSound = SoundID.Item11;
	        Item.autoReuse = true;
	        Item.shootSpeed = 14f;
	        Item.shoot = Mod.Find<ModProjectile>("OnyxSharkBomb").Type;
	        Item.useAmmo = 771;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(source, position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("OnyxSharkBomb").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
	        recipe.AddIngredient(ItemID.AdamantiteBar, 13);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 2);
	        recipe.AddIngredient(ItemID.TitaniumBar, 13);
	        recipe.AddTile(TileID.MythrilAnvil);
	        recipe.Register();
	    }
	}
}