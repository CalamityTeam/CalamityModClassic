using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassicPreTrailer.Items;
using Terraria.Audio;

namespace CalamityModClassicPreTrailer.Items.Weapons
{
	public class PlasmaRifle : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Plasma Rifle");
			// Tooltip.SetDefault("Fires a plasma blast that explodes\nRight click to change modes");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 460;
	        Item.mana = 40;
	        Item.DamageType = DamageClass.Magic;
	        Item.width = 48;
	        Item.height = 22;
	        Item.useTime = 40;
	        Item.useAnimation = 40;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 4f;
            Item.value = Item.buyPrice(1, 20, 0, 0);
            Item.rare = 10;
            Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/PlasmaBlast");
	        Item.autoReuse = true;
	        Item.shootSpeed = 12f;
	        Item.shoot = Mod.Find<ModProjectile>("PlasmaShot").Type;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 12;
		}

		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-10, 0);
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
	    
	    public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				Item.mana = 40;
	    		Item.useTime = 40;
	        	Item.useAnimation = 40;
	        	Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/PlasmaBlast");
			}
			else
			{
				Item.mana = 5;
	    		Item.useTime = 8;
	        	Item.useAnimation = 8;
	        	Item.UseSound = new SoundStyle("CalamityModClassicPreTrailer/Sounds/Item/PlasmaBolt");
			}
			return base.CanUseItem(player);
		}
	    
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
		{
	    	if (player.altFunctionUse == 2)
	    	{
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("PlasmaShot").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    	}
	    	else
	    	{
	    		Projectile.NewProjectile(Entity.GetSource_FromThis(null), position.X, position.Y, velocity.X, velocity.Y, Mod.Find<ModProjectile>("PlasmaBolt").Type, (int)((double)damage * 0.75), knockback, player.whoAmI, 0.0f, 0.0f);
	    	}
			return false;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "UeliaceBar", 7);
	        recipe.AddIngredient(ItemID.Musket);
	        recipe.AddIngredient(ItemID.ToxicFlask);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	        recipe = CreateRecipe();
	        recipe.AddIngredient(null, "UeliaceBar", 7);
	        recipe.AddIngredient(ItemID.TheUndertaker);
	        recipe.AddIngredient(ItemID.ToxicFlask);
	        recipe.AddTile(TileID.LunarCraftingStation);
	        recipe.Register();
	    }
	}
}