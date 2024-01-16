using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Eviscerator : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/3.Perforators/Eviscerator");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Eviscerator");
        Item.damage = 57;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 58;
        Item.height = 22;
        ////Tooltip.SetDefault("Fires blood clots at a high velocity");
        Item.crit += 25;
        Item.useTime = 60;
        Item.useAnimation = 60;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 7.5f;
        Item.value = 80000;
        Item.rare = 3;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("BloodClotFriendly").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 22f;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("BloodClotFriendly").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BloodSample", 8);
        recipe.AddIngredient(ItemID.Vertebrae, 4);
        recipe.AddIngredient(ItemID.CrimtaneBar, 4);
        recipe.AddTile(TileID.DemonAltar);
        recipe.Register();
	}
}}