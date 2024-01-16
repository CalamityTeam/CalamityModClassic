using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class PestilentDefiler : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/8.Plaguebringer/PestilentDefiler");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Pestilent Defiler");
        Item.damage = 135;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 56;
        Item.height = 34;
        ////Tooltip.SetDefault("Get down with the sickness\nFires exploding sickness bullets");
        Item.useTime = 37;
        Item.useAnimation = 37;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 9.5f;
        Item.value = 950000;
        Item.rare = 9;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = false;
        Item.shootSpeed = 20f;
        Item.shoot = Mod.Find<ModProjectile>("SicknessRound").Type;
        Item.useAmmo = 97;
    }
    
    public override Vector2? HoldoutOffset()
	{
		return new Vector2(0, -5);
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
    	Projectile.NewProjectile(source, position, velocity,Mod.Find<ModProjectile>("SicknessRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
    	return false;
	}

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "UeliaceBar", 8);
        recipe.AddIngredient(null, "PlagueCellCluster", 18);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}