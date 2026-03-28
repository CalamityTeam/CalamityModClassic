using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class MineralMortar : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mineral Mortar");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 30;
	        Item.DamageType = DamageClass.Ranged;
	        Item.width = 58;
	        Item.height = 26;
	        Item.useTime = 33;
	        Item.useAnimation = 33;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5f;
            Item.value = Item.buyPrice(0, 36, 0, 0);
            Item.rare = 5;
	        Item.UseSound = SoundID.Item11;
	        Item.autoReuse = true;
	        Item.shootSpeed = 14f;
	        Item.shoot = Mod.Find<ModProjectile>("OnyxSharkBomb").Type;
	        Item.useAmmo = 771;
	    }
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("OnyxSharkBomb").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
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