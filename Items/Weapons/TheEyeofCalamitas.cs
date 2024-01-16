using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class TheEyeofCalamitas : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/6.Calamitas/TheEyeofCalamitas");
		return true;
	}
	
    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.TheEyeOfCthulhu);
        //Tooltip.SetDefault("Oblivion");
        Item.damage = 45;
        Item.useTime = 22;
        Item.useAnimation = 22;
        Item.useStyle = 5;
        ////Tooltip.SetDefault("Fires brimstone lasers when enemies are near");
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 4f;
        Item.value = 500000;
        Item.rare = 6;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("TheEyeofCalamitasProjectile").Type;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectile(source, position, velocity,type, (int)((double)damage), knockback, player.whoAmI, 0.0f, 0.0f);
		return false;
	}
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CalamityDust", 5);
        recipe.AddIngredient(null, "EssenceofChaos", 3);
        recipe.AddIngredient(ItemID.Lens, 3);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
}}