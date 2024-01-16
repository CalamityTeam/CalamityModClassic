using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class SomaPrime : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/SomaPrime");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Soma Prime");
        Item.damage = 108;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 56;
        Item.height = 22;
        ////Tooltip.SetDefault("High crit. chance, extreme fire rate, and high accuracy\nFires unique high-velocity slash rounds that have a 10% chance to inflict a strong bleed debuff on hit\n50% chance to not consume ammo");
        Item.crit += 26;
        Item.useTime = 1;
        Item.useAnimation = 3;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2f;
        Item.value = 10000000;
        Item.expert = true;
        Item.UseSound = SoundID.Item40;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("SlashRound").Type; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 30f;
        Item.useAmmo = 97;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
		recipe.AddIngredient(null, "ShadowspecBar", 5);
		recipe.AddIngredient(null, "P90");
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
	}
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-10, 11) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-10, 11) * 0.05f;
	    Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), Mod.Find<ModProjectile>("SlashRound").Type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    return false;
	}
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 100) <= 50)
    		return false;
    	return true;
    }
}}