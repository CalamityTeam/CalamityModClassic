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
	public class PrimordialAncient : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Primordial Ancient");
			// Tooltip.SetDefault("An ancient relic from an ancient land");
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 385;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 20;
	        Item.width = 28;
	        Item.height = 30;
	        Item.useTime = 17;
	        Item.useAnimation = 17;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 5;
            Item.value = Item.buyPrice(1, 80, 0, 0);
            Item.rare = 10;
            Item.UseSound = SoundID.Item20;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("Ancient").Type;
	        Item.shootSpeed = 8f;
			Item.GetGlobalItem<CalamityGlobalItem>().postMoonLordRarity = 14;
		}
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "PrimordialEarth");
	        recipe.AddIngredient(ItemID.AncientBattleArmorMaterial, 10);
	        recipe.AddIngredient(null, "CosmiliteBar", 10);
	        recipe.AddIngredient(null, "Phantoplasm", 5);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}