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
	public class ManaRose : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Mana Rose");
			// Tooltip.SetDefault("Casts a mana bolt that explodes into smaller bolts");
			Item.staff[Item.type] = true;
		}

	    public override void SetDefaults()
	    {
	        Item.damage = 9;
	        Item.DamageType = DamageClass.Magic;
	        Item.mana = 8;
	        Item.width = 38;
	        Item.height = 38;
	        Item.useTime = 27;
	        Item.useAnimation = 27;
	        Item.useStyle = 5;
	        Item.noMelee = true;
	        Item.knockBack = 3.25f;
            Item.value = Item.buyPrice(0, 2, 0, 0);
            Item.rare = 2;
	        Item.UseSound = SoundID.Item109;
	        Item.autoReuse = true;
	        Item.shoot = Mod.Find<ModProjectile>("ManaBolt").Type;
	        Item.shootSpeed = 10f;
	    }
	    
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(ItemID.NaturesGift);
	        recipe.AddIngredient(ItemID.JungleRose);
	        recipe.AddIngredient(ItemID.Moonglow, 5);
	        recipe.AddTile(TileID.Anvils);
	        recipe.Register();
	    }
	}
}